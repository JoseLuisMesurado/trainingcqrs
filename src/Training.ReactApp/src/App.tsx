

import './App.css';
import MainLayout from './components/layouts/MainLayout';
import { AppRoutes } from './constants/approutes-constant';
import HomeScreen from './components/screens/home/HomeScreen';
import { Routes, Route } from "react-router-dom";
import AddPermissionScreen from './components/screens/permissions/addpermission-screen';
import UpdatePermissionScreen from './components/screens/permissions/updatepermission-screen';

const getMainLayout = () => {
  return (
      <MainLayout>
        <Routes>
          <Route path={AppRoutes.HOME} element={<HomeScreen />} />
          <Route path={AppRoutes.ANY} element={<HomeScreen />} />
          <Route path={AppRoutes.REQUESTPERMISSION} element={<AddPermissionScreen />} />
          <Route path={AppRoutes.UPDATEPERMISSION} element={<UpdatePermissionScreen />} />
        </Routes>
      </MainLayout>
  )
}

function App() {
  return (
    <>
      { getMainLayout()}
    </>
  );
}

export default App;
