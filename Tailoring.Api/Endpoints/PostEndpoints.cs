﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tailoring.Authentication;
using Tailoring.Entities;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class PostEndpoints
{
   private const  string GetPostEndPointName="Tailoring";
   private const  string SearchPost="Search";
   private const  string Category="Category";



    public static RouteGroupBuilder MapPostEndpoints(this IEndpointRouteBuilder routes){



        var group=routes.MapGroup("/posts").WithParameterValidation();

        group.MapGet("/", async (IRepository repository,int pageNumber,int pageSize) 
            => (await repository.GetAllAsync())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(post=>post.AsDto()));


        group.MapGet("/onePost",async (IRepository repository,int id)=> 
            {
                Post? post = await repository.GetAsync(id);
                return post is not null ? Results.Ok(post.AsDto()):Results.NotFound();
    
            }
        ).WithName(GetPostEndPointName);

        group.MapGet("/search", async (IRepository repository, string query,int pageNumber,int pageSize)
                =>
        {
            IEnumerable<Post> searchedPosts = await repository.SearchAsync(query);
            return searchedPosts is not null ? Results.Ok(searchedPosts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(post=>post.AsDto())):Results.NotFound();
        }).WithName(SearchPost);
        
        group.MapGet("/category", async (IRepository repository, string category,int pageNumber,int pageSize)
            =>
        {

            IEnumerable<Post> categoryPosts = await repository.GetWithCategoryAsync(category);
            return categoryPosts is not null ? Results.Ok(categoryPosts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(post=>post.AsDto())):Results.NotFound();
        }).WithName(Category);
       
        group.MapPost("/",async (
            IRepository repository,
            CreatePostDto postDto,
            ClaimsPrincipal? user
            )=>{
          //var userId=  user?.Claims?.FirstOrDefault(c => c.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?.Value;
          var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          if (userId.Equals(postDto.AuthorId.ToString()))
          {
              Post post=new (){
                  Title= postDto.Title,
                  Category = postDto.Category,
                  PostType= postDto.PostType,
                  Author= postDto.Author,
                  AuthorId= postDto.AuthorId,
                  AuthorAvatar = postDto.AuthorAvatar,
                  FeaturedImages= postDto.FeaturedImages,
                  Like = postDto.Like,
                  Video = postDto.Video,
                  Description = postDto.Description,
                  DataAdded = postDto.DataAdded,
                  LongDataAdded = postDto.LongDataAdded,
                  HaveProduct = postDto.HaveProduct

              };
              await repository.CreateAsync(post);
              return Results.CreatedAtRoute(GetPostEndPointName,new{post.Id},post);

          }

          return Results.Conflict( new { error="شما دسترسی به این کاربر ندارید"});



        }).RequireAuthorization();

        group.MapPut("/{id}",async (IRepository repository,int id,UpdatePostDto updatePostDto)=>
            {

                Post? existedPost = await repository.GetAsync(id);

                if(existedPost==null){
                    return Results.NotFound(); 
                } 
                existedPost.Title=updatePostDto.Title;
                existedPost.Category = updatePostDto.Category;
                existedPost.PostType=updatePostDto.PostType;
                existedPost.Author=updatePostDto.Author;
                existedPost.AuthorId=updatePostDto.AuthorId;
                existedPost.FeaturedImages=updatePostDto.FeaturedImages;
                existedPost.Like=updatePostDto.Like;
                existedPost.Video=updatePostDto.Video;
                existedPost.Description=updatePostDto.Description;
                existedPost.DataAdded=updatePostDto.DataAdded;
                existedPost.LongDataAdded=updatePostDto.LongDataAdded;
                existedPost.HaveProduct=updatePostDto.HaveProduct;

                await  repository.UpdateAsync(existedPost);
                return Results.NoContent();   

            }
        );

        group.MapDelete("/{id}",async (IRepository repository,int id)=>
        {
            Post? post =await repository.GetAsync(id);

            if(post is not null){
                await repository.DeleteAsync(id); 
            }

            return Results.NoContent();   
        });


        return group;

    }
    
}