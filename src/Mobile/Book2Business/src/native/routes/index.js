import React from 'react';
import { Scene, Tabs, Stack } from 'react-native-router-flux';
import { Icon } from 'native-base';

import DefaultProps from '../constants/navigation';
import AppConfig from '../../constants/config';

import RecipesContainer from '../../containers/Recipes';
import RecipesComponent from '../components/Recipes';
import RecipeViewComponent from '../components/Recipe';

import SignUpContainer from '../../containers/SignUp';
import SignUpComponent from '../components/SignUp';

import LoginContainer from '../../containers/Login';
import LoginComponent from '../components/Login';

import ForgotPasswordContainer from '../../containers/ForgotPassword';
import ForgotPasswordComponent from '../components/ForgotPassword';

import LocaleContainer from '../../containers/Locale';
import LocaleComponent from '../components/Locale';

import UpdateProfileContainer from '../../containers/UpdateProfile';
import UpdateProfileComponent from '../components/UpdateProfile';

import MemberContainer from '../../containers/Member';
import ProfileComponent from '../components/Profile';

import AboutComponent from '../components/About';

import SchedulesComponent from '../components/schedules/Schedules';

import ClientsComponent from '../components/clients/Clients';

import ReportsComponent from '../components/reports/Reports';

import MoreComponent from '../components/More';

import ServiceCategiesComponent from '../components/service_categories/ServiceCategories';
import AppointmentCategiesContainer from '../../containers/appointments/ServiceCategories';

import ServiceCategoryComponent from '../components/service_categories/ServiceCategory';
import AppointmentCategoryContainer from '../../containers/appointments/AppointmentCategory';

import ClassesComponent from '../components/classes/Classes';

import ChangeLocationComponent from '../components/locations/ChangeLocation';

import LocationMenusComponent from '../components/locations/LocationMenus';

import UpdateLocationInfoComponent from '../components/locations/UpdateLocationInfo';
import UpdateLocationInfoContainer from '../../containers/locations/UpdateLocationInfo';

import UpdateLocationContactComponent from '../components/locations/UpdateContact';
import UpdateLocationContactContainer from '../../containers/locations/UpdateContact';

import UpdateLocationAddressComponent from '../components/locations/UpdateAddress';
import UpdateLocationAddressContainer from '../../containers/locations/UpdateAddress';

import UpdateLocationGeolocationComponent from '../components/locations/UpdateGeolocation';
import UpdateLocationGeolocationContainer from '../../containers/locations/UpdateGeolocation';

import UpdateLocationImageComponent from '../components/locations/UpdateImage';
import UpdateLocationImageContainer from '../../containers/locations/UpdateImage';

const Index = (
  <Stack hideNavBar>
    <Scene hideNavBar>
      <Tabs
        key="tabbar"
        swipeEnabled
        type="replace"
        showLabel={true}
        {...DefaultProps.tabProps}
      >
        <Stack
          key="home"
          title="SCHEDULES"
          icon={() => <Icon name="calendar" {...DefaultProps.icons} />}
          {...DefaultProps.navbarProps}
        >
          <Scene key="home" component={SchedulesComponent} />
        </Stack>

        <Stack
          key="clients"
          title="CLIENTS"
          icon={() => <Icon name="contact" {...DefaultProps.icons} />}
          {...DefaultProps.navbarProps}
        >
          <Scene key="clients" component={ClientsComponent} />
        </Stack>

        <Stack
          key="reports"
          title="REPORTS"
          icon={() => <Icon name="book" {...DefaultProps.icons} />}
          {...DefaultProps.navbarProps}
        >
          <Scene key="reports" component={ReportsComponent} />
        </Stack>

        <Stack
          key="more"
          title="MORE"
          icon={() => <Icon name="more" {...DefaultProps.icons} />}
          {...DefaultProps.navbarProps}
        >
          <Scene key="more" component={MoreComponent} />
          <Scene
            back
            key="appointment_categories"
            title="APPOINTMENT CATEGORIES"
            hideNavBar={false}
            component={AppointmentCategiesContainer}
            Layout={ServiceCategiesComponent}
            {...DefaultProps.navbarProps}
          />
          <Scene
            back
            key="appointment_category"
            title="APPOINTMENT"
            hideNavBar={false}
            component={AppointmentCategoryContainer}
            Layout={ServiceCategoryComponent}
            {...DefaultProps.navbarProps}
          />
          
          <Scene back key="classes" title="CLASSES" component={ClassesComponent} />


          <Scene back key="locationMenus" title="BUSINESS LOCATION" component={LocationMenusComponent} />
          <Scene back key="locationInfo" title="Information" Layout={UpdateLocationInfoComponent} component={UpdateLocationInfoContainer} />
          <Scene back key="locationAddress" title="Address" Layout={UpdateLocationAddressComponent} component={UpdateLocationAddressContainer} />
          <Scene back key="locationContact" title="Contact" Layout={UpdateLocationContactComponent} component={UpdateLocationContactContainer} />
          <Scene back key="locationGeolocation" title="Geolocation" Layout={UpdateLocationGeolocationComponent} component={UpdateLocationGeolocationContainer} />
          <Scene back key="locationImage" title="Image" Layout={UpdateLocationImageComponent} component={UpdateLocationImageContainer} />
          
          <Scene
            back
            key="profile"
            title="PROFILE"
            component={ProfileComponent}
          />
          <Scene back key="changeLocation" title="CHANGE LOCATION" component={ChangeLocationComponent} />
          
        </Stack>
      </Tabs>
    </Scene>

    <Scene
      back
      clone
      key="recipe"
      title="RECIPE"
      {...DefaultProps.navbarProps}
      component={RecipesContainer}
      Layout={RecipeViewComponent}
    />
  </Stack>
);

export default Index;
