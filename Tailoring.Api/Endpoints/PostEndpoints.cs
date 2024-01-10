using Tailoring.Entities;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class PostEndpoints
{
   private const  string GetPostEndPointName="Tailoring";



    public static RouteGroupBuilder MapPostEndpoints(this IEndpointRouteBuilder routes){



        var group=routes.MapGroup("/search").WithParameterValidation();

        group.MapGet("/", async (IPostsRepository repository) 
            => (await repository.GetAllAsync()).Select(post=>post.AsDto()));


        group.MapGet("/{id}",async (IPostsRepository repository,int id)=> 
            {
                Post? post = await repository.GetAsync(id);
                return post is not null ? Results.Ok(post.AsDto()):Results.NotFound();
    
            }
        ).WithName(GetPostEndPointName);

        group.MapPost("/",async (IPostsRepository repository,CreatePostDto postDto)=>{

            Post post=new (){
                Title= postDto.Title,
                Category = postDto.Category,
                PostType= postDto.PostType,
                Author= postDto.Author,
                AuthorId= postDto.AuthorId,
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
        });

        group.MapPut("/{id}",async (IPostsRepository repository,int id,UpdatePostDto updatePostDto)=>
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

        group.MapDelete("/{id}",async (IPostsRepository repository,int id)=>
        {
            Post? post =await repository.GetAsync(id);

            if(post is not null){
                await repository.DeleteAsync(id); 
            }

            return Results.NoContent();   
        });


        return group;

    } }