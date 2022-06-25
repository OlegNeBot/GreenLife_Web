import axios from 'axios';
import sha256 from 'sha256';
import React, { useState } from 'react';

import Button from 'react-bootstrap/Button';
import { Card, Col, Container, Form, FormCheck, InputGroup, Modal, Row } from 'react-bootstrap';

import {IoIosArrowBack} from 'react-icons/io';
import {RiLockPasswordFill} from 'react-icons/ri';
import {MdEmail} from 'react-icons/md';

import 'bootstrap/dist/css/bootstrap.min.css';
import { LoginModel } from '../../models/LoginModel';

import { Link, useNavigate } from 'react-router-dom';
import { Paths } from '../../routes';

interface State {
	email: string,
	password: string
}

const SignIn: React.FC = () => {

  const [check, setCheck] = React.useState(false);

  const navigate = useNavigate();

  const [values, setValues] = useState<State>({
		email: '',
		password: ''
	});

  const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
		setValues({...values, [prop]: event.target.value.trim()});
	};

  const checkChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCheck(!check);
  }

  const sendData = async () => {  
    const data : LoginModel = {
      email: values.email,
      password: sha256(values.password)
    }
    const url = 'http://localhost:8080/signin';
     await axios.post(url, data)
      .then((response) => {
        if (response.status === 200) { 
          if (check) {
          localStorage.setItem('token', response.headers['token']);
          } 
          else {
            sessionStorage.setItem('token', response.headers['token']);
          }
          navigate(Paths.HomePage.path);
        } else if (response.status === 404) {
          navigate(Paths.NotFound.path);
         }
      })
      .catch((error) => {
        navigate(Paths.NotFound.path);
      });
  }

  return(
    <main>
      <section className='d-flex align-items-center my-5 mt-lg-6 mb-lg-5 w-100'>
        <Container className='w-50'>
          <p className='text-center'>
            <Card.Link as={Link} to={Paths.Landing.path}>
              <span className='text-center align-items-center d-flex'>
              <IoIosArrowBack className='text-center align-items-center d-flex '/>
                 Вернуться на главную страницу
              </span>
            </Card.Link>
          </p>
          <Row className='justify-content-center shadow-soft border rounded border-light' style={{backgroundColor: '#F5F5F5'}}>
            <Col xs={12} className='d-flex align-items-center justify-content-center'>
              <div className='shadow-soft border rounded border-light p-4 p-lg-5 w-100 '>
                <div className='text-center text-md-center mb-4 mt-md-0'>
                  <h3 className='mb-0'>Вход</h3>
                  <Form className='mt-4'>
                    <Form.Group className='mb-4'>
                      <Form.Label>Email</Form.Label>
                      <InputGroup>
                        <InputGroup.Text>
                          <MdEmail/>
                        </InputGroup.Text>
                        <Form.Control autoFocus required type="text" placeholder="Ваш email" onChange={handleChange('email')}/>
                      </InputGroup>
                    </Form.Group>
                    <Form.Group>
                      <Form.Group controlId='formBasicPassword' className='mb-4'>
                      <Form.Label>Пароль</Form.Label>
                      <InputGroup>
                        <InputGroup.Text>
                          <RiLockPasswordFill/>
                        </InputGroup.Text>
                        <Form.Control required type="password" placeholder="Ваш пароль" onChange={handleChange('password')}/>
                      </InputGroup>
                      </Form.Group>
                      <div className='d-flex justify-content-between align-items-center mb-4'>
                        <Form.Check type='checkbox'>
                          <FormCheck.Input id="defaultCheck5" className="me-2" onChange={checkChangeHandler}/>
                          <FormCheck.Label htmlFor="defaultCheck5" className="mb-0">Запомнить меня</FormCheck.Label>
                        </Form.Check>
                      </div>
                    </Form.Group>

                  <Button variant="primary" className="w-100" onClick={sendData}>
                    Войти
                  </Button>
                  </Form>

                  <div className='d-flex justify-content-center align-items-center mt-4'>
                    <span className='fw-normal'>
                      Еще не зарегистрированы? {' '}
                      <Card.Link as={Link} to={Paths.SignUp.path} className='fw-bold'>
                        {`Создать аккаунт`}
                      </Card.Link>
                    </span>
                  </div>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </main>
  );
}

export default SignIn;

