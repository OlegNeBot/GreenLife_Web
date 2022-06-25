import React from 'react';

import { render } from 'react-dom';

import Landing from './pages/Landing';

import HomePage from './pages/HomePage';
import SignIn from './components/auth/SignIn';
import SignUp from './components/auth/SignUp';
import ScrollToTop from './components/home/ScrollToTop';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NotFound from './pages/NotFound';
import { Paths } from './routes';
import Account from './pages/Account';
import CheckLists from './pages/CheckLists';
import HabitCatalog from './pages/HabitCatalog';
import HistoryAndAchievements from './pages/HistoryAndAchievements';
import MainPage from './pages/MainPage';
import Memos from './pages/Memos';
import Settings from './pages/Settings';
import CheckListHabits from './pages/CheckListHabits';
import Certificate from './components/Certificate';
import Report from './components/Report';

const root = document.getElementById('app');

render(
  <BrowserRouter>
    <ScrollToTop />
    <Routes>
      <Route path={Paths.NotFound.path} element={<NotFound />}/>
      <Route path={Paths.Landing.path} element={<Landing />}/>
      <Route path={Paths.SignIn.path} element={<SignIn />}/>
      <Route path={Paths.SignUp.path} element={<SignUp />}/>
      <Route path={Paths.Report.path} element={<Report />}/>
      <Route path={Paths.Certificate.path} element={<Certificate />}/>
      <Route path={Paths.HomePage.path} element={<HomePage />}>
        <Route index element={<MainPage />}/>
        <Route path={Paths.Account.path} element={<Account />}/>
        <Route path={Paths.CheckLists.path} >
          <Route index element={<CheckLists />}/>
          <Route path={Paths.CheckListHabits.path} element={<CheckListHabits />}/>
        </Route>
        <Route path={Paths.HabitCatalog.path} element={<HabitCatalog />}/>
        <Route path={Paths.HistoryAndAchievements.path} element={<HistoryAndAchievements />}/>
        <Route path={Paths.Memos.path} element={<Memos />}/>
        <Route path={Paths.Settings.path} element={<Settings />}/>
      </Route>
    </Routes>
  </BrowserRouter>, root);
