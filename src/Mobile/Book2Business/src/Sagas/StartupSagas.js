import { put, select } from 'redux-saga/effects'
import ScheduleActions from '../modules/chainreact/Redux/ScheduleRedux'
import LocationActions from '../modules/chainreact/Redux/LocationRedux'
import LoggedInActions, { isLoggedIn } from '../Redux/LoginRedux'
import AppStateActions from '../Redux/AppStateRedux'

export const selectLoggedInStatus = (state) => isLoggedIn(state.login)

// process STARTUP actions
export function * startup (action) {
  yield put(ScheduleActions.trackCurrentTime())
  /* ********************************************************
  * Readonly API Calls are better handled through code push *
  * *********************************************************/
  yield put(ScheduleActions.getScheduleUpdates())
  yield put(LocationActions.getNearbyUpdates())

  yield put(AppStateActions.setRehydrationComplete())
  const isLoggedIn = yield select(selectLoggedInStatus)
  if (isLoggedIn) {
    yield put(LoggedInActions.autoLogin())
  }
}
