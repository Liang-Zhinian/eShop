

interface IAppointmentSchedule {
    Id: string
    OrderDate: Date
    StartDateTime: Date
    EndDateTime: Date
    StaffId: string
    LocationId: string
    SiteId: string
    GenderPreference: string
    Duration: number
    AppointmentStatusId: number
    Notes: string
    StaffRequested: boolean
    ClientId: string
    FirstAppointment: boolean,
    AppointmentServiceItems: []
    
}

class AppointmentSchedule implements IAppointmentSchedule {
    Id: string;
    OrderDate: Date;
    StartDateTime: Date;
    EndDateTime: Date;
    StaffId: string;
    LocationId: string;
    SiteId: string;
    GenderPreference: string;
    Duration: number;
    AppointmentStatusId: number;
    Notes: string;
    StaffRequested: boolean;
    ClientId: string;
    FirstAppointment: boolean;
    AppointmentServiceItems: [];
}

export default AppointmentSchedule
