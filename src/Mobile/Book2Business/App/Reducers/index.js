import status from './status'
import member from './member'
import locale from './locale'
import locations from './locations'
import appointmentCategories from './appointmentCategories'
import appointmentTypes from './appointmentTypes'
import staffs from './staffs'
import nearby from './nearby'
import theme from './theme'

const rehydrated = (state = false, action) => {
  switch (action.type) {
    case 'persist/REHYDRATE':
      return true
    default:
      return state
  }
}

export default {
  // rehydrated,
  status,
  theme,
  member,
  nearby,
  locale,
  locations,
  appointmentCategories,
  appointmentTypes,
  staffs
}
