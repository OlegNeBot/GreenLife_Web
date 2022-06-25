import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import Navbar from '../components/home/Navbar';
import Sidebar from '../components/home/Sidebar';

import 'bootstrap/dist/css/bootstrap.min.css';
import { Outlet } from 'react-router-dom';

const HomePage : React.FC = () => {
    return (
      <Container fluid>
      <Row>
        <Col md={3} className='px-0'>
        <Sidebar />
        </Col>
        <Col className='px-0 pb-3' style={{position: 'relative'}} >
        <Navbar />
        <Outlet />
        </Col>
      </Row>
    </Container>
    );
}

export default HomePage;