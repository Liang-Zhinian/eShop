import { combineReducers } from 'redux'
import configureStore from './CreateStore'
import rootSaga from '../Sagas/'
import { AppNavigation } from '../Navigation/AppNavigation'
import Other from '../Reducers'

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
    schedule: require('./ScheduleRedux').reducer,
    location: require('./LocationRedux').reducer,
    notifications: require('./NotificationRedux').reducer,
    ...Other
  })

  return configureStore(rootReducer, rootSaga)
}
