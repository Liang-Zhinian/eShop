import Store from '../Store/appointmentTypes'

export const initialState = Store

export default function appointmentTypeReducer (state = initialState, action) {
  switch (action.type) {
    case 'APPOINTMENT_TYPES_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'APPOINTMENT_TYPES_REPLACE': {
      let appointmentTypes = {}

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        appointmentTypes = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        appointmentTypes,
        selectedAppointmentType: null
      }
    }
    case 'SET_SELECTED_APPOINTMENT_TYPE': {
      return {
        ...state,
        selectedAppointmentType: action.data
      }
    }
    default:
      return state
  }
}
