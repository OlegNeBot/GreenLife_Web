import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import Navbar from '../components/home/Navbar';
import Sidebar from '../components/home/Sidebar';

import 'bootstrap/dist/css/bootstrap.min.css';
import Account from './Account';
import Settings from './Settings';
import MainPage from './MainPage';
import HistoryAndAchievements from './HistoryAndAchievements';
import HabitCatalog from './HabitCatalog';
import NotFound from '../components/NotFound';
import { Routes, Route, useNavigate, Outlet } from 'react-router-dom';
import { Paths } from '../routes';
import SignIn from '../components/auth/SignIn';
import SignUp from '../components/auth/SignUp';
import Landing from './Landing';
import CheckLists from './CheckLists';
import Memos from './Memos';

// const RouteWithSidebar : React.FC<IHomeable> = props => {
//   return(
    
//     <Container fluid>
//             <Row>
//               <Col md={3} className='px-0'>
//               <Sidebar />
//               </Col>
//               <Col className='px-0 pb-3' >
//               <Navbar />
//               {/* TODO: Add routes */}
//               {/* <Account /> */}
//               {/* <Settings /> */}
//               {/* <MainPage /> */}
//               {/* <HistoryAndAchievements /> */}
              
//               {/* <HabitCatalog /> */}
//               <Route path={props.path} element={props.element}></Route>
//               </Col>
//             </Row>
//           </Container>
//   );

// }


const HomePage : React.FC = () => {
  const navigate = useNavigate();
    return (
      <Container fluid>
      <Row>
        <Col md={3} className='px-0'>
        <Sidebar />
        </Col>
        <Col className='px-0 pb-3' >
        <Navbar />
        <Outlet />
        </Col>
      </Row>
    </Container>
    );
}

export default HomePage;