// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Modeling
{
    //Отдел электриков
    public class ElectricianDepartment : Department
    {
        protected override bool CanHandle(Request request)
        {
            return request is ElectricianRequest;
        }

        protected sealed override Employee CreateEmployee()
        {
            return new Electrician(NameGenerator.GetName());
        }
    }
}