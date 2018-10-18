import Store from '../Store/staffSchedules'

export const initialState = Store

export default function staffSchedulesReducer (state = initialState, action) {
  switch (action.type) {
    case 'STAFF_SCHEDULES_ERROR': {
      return {
        ...state,
        loading: false,
        error: action.data
      }
    }
    case 'STAFF_SCHEDULES_FETCHING_STATUS': {
      return {
        ...state,
        loading: action.data,
        error: null
      }
    }
    case 'STAFF_SCHEDULES_REPLACE': {
      let schedules = {}

      // Pick out the props I need
      if (action.data && typeof action.data === 'object') {
        schedules = action.data
      }

      return {
        ...state,
        error: null,
        loading: false,
        staffSchedules: schedules
      }
    }
    case 'SET_SELECTED_SCHEDULE_ITEM': {
      return {
        ...state,
        selectedScheduleItem: action.data
      }
    }
    default:
      return state
  }
}
