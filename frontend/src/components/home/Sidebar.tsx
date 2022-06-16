import React, {useState} from 'react';
import { Button, Card, Container, NavItem } from 'react-bootstrap';
import Nav from 'react-bootstrap/esm/Nav';
import Navbar from 'react-bootstrap/esm/Navbar';

import { BsCardChecklist, BsInfoCircle, BsBoxArrowRight, BsFillJournalBookmarkFill, BsArchive, BsPatchQuestion } from 'react-icons/bs';
import { NavLink } from 'react-router-dom';
import { Paths } from '../../routes';

export default () => {
    return (
      <>
      <style type='text/css'>
        {`
        .sidebar {
          min-height: 100vh;
          position: fixed;
        }
        .sidebar, .sidebar-card {
          background-color: #4d7d0f;
        }
        .sidebar-card {
          border: none;
          color: white;
        }
        .icon {
          text-align: center;
	        display: inline-flex;
          align-items: center;
          justify-content: center;
        }
        `}
      </style>
      <Navbar className='d-block  sidebar' expand='md' >
        <Nav className='flex-column pt-3 pt-md-0 justify-content-between' style={{minHeight: '400px'}}>
          <Navbar.Brand className='ps-4'>
            <Nav.Link as={NavLink} to={Paths.HomePage.path}>
              <h1 className='text-white'>GreenLife</h1>
            </Nav.Link>
          </Navbar.Brand>
            <NavItem>
              <Card className='sidebar-card'>
                <Nav.Link as={NavLink} to={Paths.HabitCatalog.path} className='ps-4 fs-4'>
              <span className='text-white align-items-center d-flex'>
                <BsFillJournalBookmarkFill className='icon me-3'/>
                Каталог привычек
              </span>
              </Nav.Link>
              </Card>
            </NavItem>
            <Nav.Item>
            <Card className='sidebar-card'>
                <Nav.Link as={NavLink} to={Paths.CheckLists.path} className='ps-4 fs-4'>
              <span className='text-white align-items-center d-flex'>
              <BsCardChecklist className='icon me-3'/>
              Чек-листы
              </span>
              </Nav.Link>
              </Card>
            </Nav.Item>
            <Nav.Item>
              <Card className='sidebar-card'>
                <Nav.Link as={NavLink} to={Paths.Memos.path} className='ps-4 fs-4'>
              <span className='text-white align-items-center d-flex'>
              <BsInfoCircle className='icon me-3'/>
              Памятки
              </span>
              </Nav.Link>
              </Card>
            </Nav.Item>
            <Nav.Item>
              <Card className='sidebar-card'>
                <Nav.Link as={NavLink} to={Paths.HistoryAndAchievements.path} className='ps-4 fs-4'>
              <span className='text-white align-items-center d-flex'>
                <BsArchive className='icon me-3'/>
                История и достижения
              </span>
              </Nav.Link>
              </Card>
            </Nav.Item>

            {/* <Nav.Item className='mt-20'>
              <Card className='sidebar-card '>
                <Nav.Link className='ps-4 fs-4'>
                  
                  <span className='text-white align-items-center d-flex'>
                  <BsBoxArrowRight className='icon me-3'/>
                    Выход
                  </span>
                </Nav.Link>
              </Card>
            </Nav.Item> */}
          </Nav>
      </Navbar>
      </>
    );
}