using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HR.LeaveManagement.Application.features.LeaveType.Queries.GetAllLeaveType
{
    //public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
    //{
    //}

    //الفرق بين الكلاس والريكورد ان الريكورد لا يمكن تكراره ولا يمكن تعديله بعد انشاءه، بينما الكلاس يمكن تكراره وتعديله، الريكورد يستخدم في الحالات التي نريد فيها تمثيل بيانات ثابتة لا تتغير، بينما الكلاس يستخدم في الحالات التي نريد فيها تمثيل بيانات متغيرة. في هذا السياق، استخدام الريكورد مناسب لأنه يمثل طلب ثابت للحصول على قائمة من أنواع الإجازات ولا يحتاج إلى تعديل بعد إنشائه.

    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
