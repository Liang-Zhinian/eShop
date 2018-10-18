import React from 'react';
import PropTypes from 'prop-types'
import { connect } from 'react-redux'

import Layout from './Components/StaffSchedule'
import * as StaffScheduleActions from '../../Actions/staffSchedules'

class StaffSchedule extends React.Component {
    static propTypes = {
        onFormSubmit: PropTypes.func.isRequired,
        isLoading: PropTypes.bool.isRequired,
        member: PropTypes.shape({}).isRequired,
        appointmentTypes: PropTypes.shape({}).isRequired,
        appointmentCategories: PropTypes.shape({}).isRequired,
        staffSchedules: PropTypes.shape({}).isRequired,
    }

    state = {
        errorMessage: null,
        successMessage: null
    }

    schedule = {
        Id: '4',
        IsAvailability: true,
        StartDateTime: new Date('2018-10-13 08:30'),
        EndDateTime: new Date('2018-10-30 22:30'),
        StaffId: 'xxxxxx',
        ServiceItemId: 'xxxx',
        LocationId: 'xxxx',
        SiteId: 'vj4ueTKK',
        BookableEndDateTime: 'vj4ueTKK',
        Sunday: true,
        Monday: true,
        Tuesday: true,
        Wednesday: false,
        Thursday: true,
        Friday: true,
        Saturday: true,
    };

    constructor(props) {
        super(props)
        this.actionType = props.navigation.getParam('ActionType') || 'Add'
    }

    onFormSubmit = (data) => {
        const { onFormSubmit } = this.props
        return onFormSubmit(data)
            .then(mes => this.setState({ successMessage: mes, errorMessage: null }))
            .catch((err) => { this.setState({ errorMessage: err, successMessage: null }); throw err })
    }

    render = () => {
        const {
            member,
            staffSchedules,
            appointmentTypes,
            isLoading,
        } = this.props

        console.log(member)

        const { successMessage, errorMessage } = this.state

        let staffSchedule = this.actionType == 'Update' ? staffSchedules.selectedStaffSchedule : {
            StaffId: member.Id,
            LocationId: member.currentLocation.LocationId,
            ServiceItemId: appointmentTypes.selectedAppointmentType.Id,
            SiteId: member.SiteId,
            Id: '',
            IsAvailability: true,
            StartDateTime: new Date(),
            EndDateTime: new Date(),
            BookableEndDateTime: new Date(),
            Sunday: false,
            Monday: false,
            Tuesday: false,
            Wednesday: false,
            Thursday: false,
            Friday: false,
            Saturday: false,
        }

        return (
            <Layout
                staffSchedule={staffSchedule}
                loading={isLoading}
                error={errorMessage}
                success={successMessage}
                onFormSubmit={this.onFormSubmit}
            />
        )
    }
}

const mapStateToProps = state => ({
    member: state.member || {},
    appointmentTypes: state.appointmentTypes || {},
    appointmentCategories: state.appointmentCategories || {},
    staffSchedules: state.staffSchedules || {},
    isLoading: state.status.loading || false,
})

const mapDispatchToProps = {
    onFormSubmit: StaffScheduleActions.addOrUpdateAvailability
}

export default connect(mapStateToProps, mapDispatchToProps)(StaffSchedule)