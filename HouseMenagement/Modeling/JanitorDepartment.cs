// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace Modeling
{
    //Отдел уборщиков
    public class JanitorDepartment : Department
    {
        protected override bool CanHandle(Request request)
        {
            return request is JanitorRequest;
        }

        protected sealed override Employee CreateEmployee()
        {
            return new Janitor(NameGenerator.GetName());
        }
    }
}