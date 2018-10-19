

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
    FirstAppointment: boolean
    
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
}

export default AppointmentSchedule
