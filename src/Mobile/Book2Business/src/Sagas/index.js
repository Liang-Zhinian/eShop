import { takeLatest } from 'redux-saga/effects'

/* ------------- Types ------------- */

import { StartupTypes } from '../Redux/StartupRedux'
import { LoginTypes } from '../Redux/LoginRedux'
import { ScheduleTypes } from '../modules/chainreact/Redux/ScheduleRedux'
import { LocationTypes } from '../modules/chainreact/Redux/LocationRedux'

/* ------------- Sagas ------------- */

import { startup } from './StartupSagas'
import { login } from './LoginSagas'
import { trackCurrentTime } from '../modules/chainreact/Sagas/ScheduleSagas'
import { visitGithub, visitTwitter } from './SocialSagas'
import { getScheduleUpdates } from '../modules/chainreact/Sagas/ScheduleUpdateSagas'
import { getNearbyUpdates } from '../modules/chainreact/Sagas/LocationSagas'

/* ------------- API ------------- */

import DebugConfig from '../Config/DebugConfig'
import API from '../modules/chainreact/Services/Api'
import FixtureAPI from '../modules/chainreact/Services/FixtureApi'
// const api = API.create()

// The API we use is only used from Sagas, so we create it here and pass along
// to the sagas which need it.
const api = DebugConfig.useFixtures ? FixtureAPI : API.create()

/* ------------- Connect Types To Sagas ------------- */

export default function * root () {
  let sagaIndex = [
    // some sagas only receive an action
    takeLatest(StartupTypes.STARTUP, startup),
    takeLatest(LoginTypes.LOGIN_REQUEST, login),
    takeLatest(ScheduleTypes.TRACK_CURRENT_TIME, trackCurrentTime),
    takeLatest(ScheduleTypes.VISIT_GITHUB, visitGithub),
    takeLatest(ScheduleTypes.VISIT_TWITTER, visitTwitter)
  ]

  // debug conditional API calls
  if (DebugConfig.getAPI) {
    sagaIndex.push(takeLatest(ScheduleTypes.GET_SCHEDULE_UPDATES, getScheduleUpdates, api))
    sagaIndex.push(takeLatest(LocationTypes.GET_NEARBY_UPDATES, getNearbyUpdates, api))
  }

  yield sagaIndex
}
