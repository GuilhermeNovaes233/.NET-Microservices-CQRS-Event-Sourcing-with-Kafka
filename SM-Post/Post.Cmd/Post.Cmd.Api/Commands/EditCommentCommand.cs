using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
	public class EditCommentCommand : BaseCommand
	{
		public Guid CommentId { get; set; }
		public string Command { get; set; }
		public string UserName { get; set; }
	}
}