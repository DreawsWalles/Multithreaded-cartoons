// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Modeling
{
    //Отдел сантехников
    public class PlumberDepartment : Department
    {
        protected override bool CanHandle(Request request)
        {
            return request is PlumberRequest;
        }

        protected sealed override Employee CreateEmployee()
        {
            return new Plumber(NameGenerator.GetName());
        }
    }
}