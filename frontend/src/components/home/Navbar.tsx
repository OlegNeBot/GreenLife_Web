import { observer } from 'mobx-react-lite';
import React, {useEffect, useState} from 'react';
import { Container, Dropdown, Nav, Navbar } from 'react-bootstrap';
import DropdownItem from 'react-bootstrap/esm/DropdownItem';
import DropdownMenu from 'react-bootstrap/esm/DropdownMenu';
import DropdownToggle from 'react-bootstrap/esm/DropdownToggle';
import { BsFillPersonFill,  BsFillBellFill, BsFillGearFill, BsBoxArrowRight} from 'react-icons/bs';

import { Link } from 'react-router-dom';
import { Paths } from '../../routes';

import account from '../../store/AccountStore';

export default observer(() => {
  
  const exit = () => {
    localStorage.clear();
    sessionStorage.clear();
  }

  useEffect(() => {
    account.load();
    account.notifyUser(account.Account.Name);
  })
  
  
  return(
    <>
    <style>
      {`
        .icon {
          text-align: center;
	        display: inline-flex;
          align-items: center;
          justify-content: center;
        }
      `}
    </style>
    <Navbar bg='' className="ps-0 pe-2 pb-0 border-bottom">
      <Container fluid className='px-0'>
        <div className='d-flex justify-content-end w-100'>
          <Nav className='align-items-center pe-4'>
            <Dropdown as={Nav.Item}>
              <DropdownToggle as={Nav.Link} className='text-dark me-lg-3 text-white' onClick={() => {account.notifyUser(account.Account.Name);}}>
                <span className='align-items-center text-black'>
                  <BsFillBellFill className='icon'/>
                </span>
              </DropdownToggle>
            </Dropdown>
            <Dropdown as={Nav.Item} className='pe-2'>
              <DropdownToggle as={Nav.Link} className='pt-1 px-0 text-white d-flex justify-content-between'>
                <div className='align-items-center'>
                  <div className=' ms-2 text-dark align-items-center d-none d-lg-block'>
                    <span className='mb-0 font-small fw-bold align-items-center'>
                      {account.Account.Name}
                    </span>
                  </div>
                </div>
              </DropdownToggle>
              <DropdownMenu className='mt-2'>
                <DropdownItem as={Link} to={Paths.Account.path} className='fw-bold'>
                  <span className='text-black align-items-center d-flex'><BsFillPersonFill className='icon me-1'/>Профиль</span>
                </DropdownItem>
                <DropdownItem as={Link} to={Paths.Settings.path} className='fw-bold'>
                <span className='text-black align-items-center d-flex'><BsFillGearFill className='icon me-1'/>Настройки</span>
                </DropdownItem>

                <Dropdown.Divider />

                <DropdownItem as={Link} to={Paths.Landing.path} className='fw-bold' onClick={exit}>
                <span className='text-danger align-items-center d-flex '><BsBoxArrowRight className='icon me-1'/>Выйти</span>
                </DropdownItem>
              </DropdownMenu>
            </Dropdown>
          </Nav>
        </div>
      </Container>
    </Navbar>
    </>
  );
});