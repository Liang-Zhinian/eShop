import { combineReducers } from 'redux'
import configureStore from './CreateStore'
import rootSaga from '../Sagas/'
import AppNavigation from '../Navigation/AppNavigation'
import Booking from '../modules/booking/Reducers'

const navReducer = (state, action) => {
  const newState = AppNavigation.router.getStateForAction(action, state)
  return newState || state
}

export default () => {
  /* ------------- Assemble The Reducers ------------- */
  const rootReducer = combineReducers({
    nav: navReducer,
    // nav: require('./NavigationRedux').reducer,
    appState: require('./AppStateRedux').reducer,
    login: require('./LoginRedux').reducer,
    schedule: require('../modules/chainreact/Redux/ScheduleRedux').reducer,
    location: require('../modules/chainreact/Redux/LocationRedux').reducer,
    notifications: require('./NotificationRedux').reducer,
    ...Booking
  })

  return configureStore(rootReducer, rootSaga)
}
