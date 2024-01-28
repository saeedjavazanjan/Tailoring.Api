using Tailoring.Entities;
using Tailoring.Repository;

namespace Tailoring.Endpoints;



public static class CommentsEndPoints
{
    
    private const  string GetPostComments="PostComments";

        public static RouteGroupBuilder MapCommentsEndPoints(this IEndpointRouteBuilder routes){



        var group=routes.MapGroup("/comments").WithParameterValidation();
        

        group.MapGet("/postComments", async (IRepository repository, int postId)
                =>
        {
            IEnumerable<Comment> postComments = await repository.GetPostCommentsAsync(postId);
            return postComments is not null ? Results.Ok(postComments
                .Select(post=>post.AsDto())):Results.NotFound();
        }).WithName(GetPostComments);
        
        group.MapPost("/",async (IRepository repository,AddCommentDto commentDto)=>{

            Comment comment=new (){
                UserId= commentDto.UserId,
                PostId = commentDto.PostId,
                Date= commentDto.Date,
                CommentText= commentDto.CommentText,
               UserName = "",
               Avatar = ""
            };
            await repository.AddCommentAsync(comment);
            return Results.Ok("Saved Successfully");
        });

        group.MapPut("/{id}",async (IRepository repository,int id,UpdateCommentDto updatedCommentDto)=>
            {

                Comment? existedComment = await repository.GetCommentAsync(id);

                if(existedComment==null){
                    return Results.NotFound(); 
                }

                existedComment.CommentText = updatedCommentDto.CommentText;
                existedComment.Date = updatedCommentDto.Date;

                await  repository.UpdateCommentAsync(existedComment);
                return Results.Ok("updated successfully");

            }
        );

        group.MapDelete("/{id}",async (IRepository repository,int id)=>
        {
            Comment? comment =await repository.GetCommentAsync(id);

            if(comment is not null){
                await repository.DeleteComments(id); 
            }
            return Results.NoContent();   
        });


        return group;

    }

}