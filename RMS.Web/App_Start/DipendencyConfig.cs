using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using RMS.BLL.IManager;
using RMS.BLL.Manager;

namespace RMS.Web.App_Start
{
    public class DipendencyConfig
    {
        private static IContainer _container;

        public static void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AccountInformationManager>().As<IAccountInformationManager>().InstancePerRequest();
            builder.RegisterType<BranchInfoManager>().As<IBranchInfoManager>().InstancePerRequest();
            builder.RegisterType<CommonCodeManager>().As<ICommonCodeManager>().InstancePerRequest();
            builder.RegisterType<ControlShipInfoManager>().As<IControlShipInfoManager>().InstancePerRequest();
            builder.RegisterType<DamageMachineriesIfoManager>().As<IDamageMachineriesInfoManager>().InstancePerRequest();
            builder.RegisterType<DeckInfoManager>().As<IDeckInfoManager>().InstancePerRequest();
            builder.RegisterType<DocumentationManager>().As<IDocumentationManager>().InstancePerRequest();
            builder.RegisterType<EventLogManager>().As<IEventLogManager>().InstancePerRequest();
            builder.RegisterType<FuelConsumptionManager>().As<IFuelConsumptionManager>().InstancePerRequest();
            builder.RegisterType<LubOilConsumptionManager>().As<ILubOilConsumptionManager>().InstancePerRequest();
            builder.RegisterType<MachineryInfoManager>().As<IMachineryInfoManager>().InstancePerRequest();
            builder.RegisterType<MachineryRunningManager>().As<IMachineryRunningManager>().InstancePerRequest();
            builder.RegisterType<MessageInfoManager>().As<IMessageInfoManager>().InstancePerRequest();
            builder.RegisterType<ReportingManager>().As<IReportingManager>().InstancePerRequest();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerRequest();
            builder.RegisterType<ShipInfoManager>().As<IShipInfoManager>().InstancePerRequest();
            builder.RegisterType<ShipDetailManager>().As<IShipDetailManager>().InstancePerRequest();
            builder.RegisterType<ShipMovementManager>().As<IShipMovementManager>().InstancePerRequest();
            builder.RegisterType<ShipPowerManager>().As<IShipPowerManager>().InstancePerRequest();
            builder.RegisterType<ShipSpeedTrialManager>().As<IShipSpeedTrialManager>().InstancePerRequest();
            builder.RegisterType<UserURLRightManager>().As<IUserURLRightManager>().InstancePerRequest();
            builder.RegisterType<UserLogInfoManager>().As<IUserLogInfoManager>().InstancePerRequest();
            builder.RegisterType<UserLogInfoManager>().As<IUserLogInfoManager>().InstancePerRequest();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerRequest();
            builder.RegisterType<DeckInfoManager>().As<IDeckInfoManager>().InstancePerRequest();
            builder.RegisterType<MachinaryEquipmentInformationManager>().As<IMachinaryEquipmentInformationManager>().InstancePerRequest();
            builder.RegisterType<ShipEmployedManager>().As<IShipEmployedManager>().InstancePerRequest();
            builder.RegisterType<RefitDockingScheduleManager>().As<IRefitDockingScheduleManager>().InstancePerRequest();
            builder.RegisterType<AsAsInfoManager>().As<IAsAsInfoManager>().InstancePerRequest();
            builder.RegisterType<MajorDemandOrProcureManager>().As<IMajorDemandOrProcureManager>().InstancePerRequest();
            builder.RegisterType<BooksAndBrManager>().As<IBooksAndBrManager>().InstancePerRequest();
            builder.RegisterType<ProcurementScheduleManager>().As<IProcurementScheduleManager>().InstancePerRequest();
            builder.RegisterType<TrainingInfoManager>().As<ITrainingInfoManager>().InstancePerRequest();
            builder.RegisterType<FortnightlyInfoManager>().As<IFortnightlyInfoManager>().InstancePerRequest();
            builder.RegisterType<FortnightlyDetailsManager>().As<IFortnightlyDetailsManager>().InstancePerRequest();
            builder.RegisterType<ShipCraftManager>().As<IShipCraftManager>().InstancePerRequest();
            builder.RegisterType<ShipInactiveManager>().As<IShipInactiveManager>().InstancePerRequest();
            builder.RegisterType<ShipInactiveOrgManager>().As<IShipInactiveOrgManager>().InstancePerRequest();
            builder.RegisterType<ICEManager>().As<IICEManager>().InstancePerRequest();
            builder.RegisterType<ShipcommOrgnaizationManager>().As<IShipcommOrgnaizationManager>().InstancePerRequest();
            builder.RegisterType<vwNotificationManager>().As<IvwNotificationManager>().InstancePerRequest();
            builder.RegisterType<UpdateOplStateManager>().As<IUpdateOplStateManager>().InstancePerRequest();
            builder.RegisterType<SignalManager>().As<ISignalManager>().InstancePerRequest();
            builder.RegisterType<NewMachinaryEquipmentInformation_ResultManager>().As<INewMachinaryEquipmentInformation_ResultManager>().InstancePerRequest();
            builder.RegisterType<RunningHourInfoManager>().As<IRunningHourInfoManager>().InstancePerRequest();
            builder.RegisterType<RefitDockingReportExcellManager>().As<IRefitDockingReportExcellManager>().InstancePerRequest();

            builder.RegisterFilterProvider();
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private static T Resolve<T>()
        {
            if (_container == null)
                SetupContainer();

            return _container.Resolve<T>();
        }
    }
}