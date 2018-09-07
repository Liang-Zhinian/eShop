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

import ContactInformationComponent from '../components/business/ContactInformation';

import UpdateLocationComponent from '../components/business/UpdateLocation';

import BusinessInformationComponent from '../components/business/BusinessInformation';

import ChangeLocationComponent from '../components/business/ChangeLocation';

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
          <Scene back key="businessInformation" title="BUSINESS INFORMATION" component={BusinessInformationComponent} />
          <Scene back key="location" title="BUSINESS LOCATION" component={UpdateLocationComponent} />
          
          {/*<Stack
            key="businessInformation"
            title="BUSINESS INFORMATION"
            {...DefaultProps.navbarProps}
          >
            <Scene back key="businessInformation" title="BUSINESS INFORMATION" component={BusinessInformationComponent} />
          </Stack>*/}

          <Scene back key="contactInformation" title="CONTACT INFORMATION" component={ContactInformationComponent} />
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
