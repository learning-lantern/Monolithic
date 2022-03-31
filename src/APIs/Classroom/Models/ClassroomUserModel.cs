using APIs.User.Models;

namespace APIs.Classroom.Models
{
    public class ClassroomUserModel
    {
        public int ClassroomId { get; set; }
        public ClassroomModel Classroom { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public UserModel User { get; set; } = null!;

        public ClassroomUserModel() { }
        public ClassroomUserModel(int classroomId, string userId)
        {
            ClassroomId = classroomId;
            UserId = userId;
        }
    }
}
