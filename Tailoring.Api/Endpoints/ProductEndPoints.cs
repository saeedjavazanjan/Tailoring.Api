using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tailoring.Entities;
using Tailoring.Repository;

namespace Tailoring.Endpoints;

public static class ProductEndPoints
{
    private const  string GetProductEndPointName="Product";

        public static RouteGroupBuilder MapProductEndPoints(this IEndpointRouteBuilder routes){



        var group=routes.MapGroup("/products").WithParameterValidation();

        /*group.MapGet("/", async (IRepository repository,int pageNumber,int pageSize) 
            => (await repository.GetAllAsync())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(post=>post.AsDto()));*/


        group.MapGet("/oneProduct",async (IRepository repository,int id)=> 
            {
                Product? product = await repository.GetProductOfPostAsync(id);
                return product is not null ? Results.Ok(product.AsDto()):Results.NotFound(new{error="مورد یافت نشد"});
    
            }
        ).WithName(GetProductEndPointName);

        /*group.MapGet("/search", async (IRepository repository, string query,int pageNumber,int pageSize)
                =>
        {
            IEnumerable<Post> searchedPosts = await repository.SearchAsync(query);
            return searchedPosts is not null ? Results.Ok(searchedPosts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(post=>post.AsDto())):Results.NotFound();
        }).WithName(SearchPost);*/
        
        /*group.MapGet("/category", async (IRepository repository, string category,int pageNumber,int pageSize)
            =>
        {

            IEnumerable<Post> categoryPosts = await repository.GetWithCategoryAsync(category);
            return categoryPosts is not null ? Results.Ok(categoryPosts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(post=>post.AsDto())):Results.NotFound();
        }).WithName(Category);*/
       
        group.MapPost("/uploadProduct",async (
            IFileService iFileService,
            IRepository iRepository,
            [FromForm] AddProductDto productDto,
            ClaimsPrincipal? user
            )=>{
          var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          User? currentUser = await iRepository.GetUserAsync(Int32.Parse(userId));
          if(currentUser==null){
              return Results.NotFound(new{error="کاربر یافت نشد."}); 
          }

          var post =await iRepository.GetAsync(productDto.PostId);
          if (post.AuthorId != Int32.Parse(userId))
          {
              return Results.Conflict( new { error="شما دسترسی به این کاربر ندارید"});

          }

          if (userId.Equals(currentUser.UserId.ToString()))
          {
             
              
              
              Product product=new (){
                  Name= productDto.Name,
                  Description = productDto.Description,
                  TypeOfProduct= productDto.TypeOfProduct,
                  Mas= productDto.Mas,
                  Supply= productDto.Supply,
                  Unit = productDto.Unit,
                  Price= productDto.Price,
                  PostId = productDto.PostId,
                  Images = [],
                  AttachedFile = productDto.AttachedFile,
              };
              await iRepository.CreateProductAsync(product);

              Product? existedProduct = await iRepository.GetProductAsync(product.Id);
              
              if (productDto.Images != null &&
                  productDto.Images.Count>0

                  )
              {
                  var fileResult = 
                      iFileService.SaveProductImages(productDto.Images, product.Id.ToString());
                  if (fileResult.Item1==1)
                  {
                      existedProduct!.Images = fileResult.Item2;
                  }
              }
              

              await iRepository.UpdateProductAsync(existedProduct!);

              return Results.CreatedAtRoute(GetProductEndPointName,new{product.Id},product);
              
              
          }

          return Results.Conflict( new { error="شما دسترسی به این کاربر ندارید"});



        }).RequireAuthorization().DisableAntiforgery();

        group.MapPut("update/{id}",async (
                IRepository repository,
                int id, 
                UpdateProductDto updateProductDto,
                ClaimsPrincipal? user)=>
            {
                var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Product? existedProduct = await repository.GetProductAsync(id);
                if(existedProduct==null){
                    return Results.NotFound(new{error="محصول مورد نظر یافت نشد"}); 
                } 
                if(existedProduct!=null){
                    Post? existedPost = await repository.GetAsync(existedProduct.PostId);
                    if (existedPost.AuthorId != Int32.Parse(userId))
                    {
                        return Results.Unauthorized();
                    }
                } 
                
               
                existedProduct.Name=updateProductDto.Name;
                existedProduct.Description = updateProductDto.Description;
                existedProduct.Mas=updateProductDto.Mas;
                existedProduct.Supply=updateProductDto.Supply;
                existedProduct.Unit=updateProductDto.Unit;
                existedProduct.Price=updateProductDto.Price;
                
                await  repository.UpdateProductAsync(existedProduct);
                return Results.Ok("با موفقیت به روز رسانی شد");   

            }
        ).RequireAuthorization();

        group.MapDelete("/{id}",async (
            IFileService fileService,
            IRepository repository,
            int id,
            ClaimsPrincipal? user
        )=>
        {
            var userId= user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Product? product =await repository.GetProductAsync(id);
            Post? post =await repository.GetAsync(product.PostId);


            if (post.AuthorId == Int32.Parse(userId))
            {
                if(product is not null){
                   
                        fileService.DeleteProductImage(id.ToString());
                    
                    await repository.DeleteProductAsync(id);

                    
                    
                    return Results.Ok();
                }
                return Results.NoContent();   

            }

            return Results.Unauthorized();


        }).RequireAuthorization();


        return group;

    }

}