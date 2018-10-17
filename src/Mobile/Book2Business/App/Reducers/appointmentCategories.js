import Store from '../Store/appointmentCategories'

export const initialState = Store

export default function serviceCategoryReducer (state = initialState, action) {
  switch (action.type) {
    case 'APPOINTMENT_CATEGORIES_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'APPOINTMENT_CATEGORIES_REPLACE': {
      let appointmentCategories = []

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        appointmentCategories = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        appointmentCategories,
        selectedAppointmentCategory: null,
      }
    }
    case 'SET_SELECTED_APPOINTMENT_CATEGORY': {
      return {
        ...state,
        selectedAppointmentCategory: action.data
      }
    }
    default:
      return state
  }
}
