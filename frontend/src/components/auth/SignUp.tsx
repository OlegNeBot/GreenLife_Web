import React, { useEffect, useState } from 'react';
import axios from 'axios';
import sha256 from 'sha256';

import { Container, Card, Row, Col, Form, InputGroup, FormCheck, Button, Modal } from 'react-bootstrap';

import { IoIosArrowBack } from 'react-icons/io';
import { MdDriveFileRenameOutline, MdEmail } from 'react-icons/md';
import { RiLockPasswordFill, RiLockPasswordLine } from 'react-icons/ri';

import 'bootstrap/dist/css/bootstrap.min.css';
import { RegistrationModel } from '../../models/RegistrationModel';
import { Paths } from '../../routes';
import { Link, useNavigate } from 'react-router-dom';

interface State {
	name: string,
	email: string,
	password: string,
}

const SignUp: React.FC = () => {
    //User data
    const [values, setValues] = useState<State>({
      name: '',
      email: '',
      password: '',
    })

    const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
      setValues({...values, [prop]: event.target.value.trim()});
    };

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    let errorMsg = '';

    const navigate = useNavigate();

    const [valid, setValid] = useState(false);

    const handleSubmit = (event:React.FormEvent<HTMLFormElement>) => {
      const form = event.currentTarget;
      if (form.checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
      }
        setValid(true);
        sendData();
    };
    
    const sendData = () => {
      const data : RegistrationModel = {
        name: values.name,
        email: values.email,
        password: sha256(values.password),
      }
      const url = 'http://localhost:8080/signup';
      axios.post(url, data)
      .then((response) => {
        if (response.status === 200) { 
          sessionStorage.setItem('token', response.headers['token']);
          navigate(Paths.HomePage.path); 
        } 
        else if (response.status === 409) {
          errorMsg = response.headers['Error'];
          setShow(true);
        }
      })
      .catch((error) => {
        errorMsg = 'Произошла ошибка! Проверьте введенные данные и повторите попытку.';
        setShow(true);
      });
    }

    return (
      <main>
      <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5 w-100">
        <Container className='w-50'>
          <p className="text-center">
            <Card.Link as={Link} to={Paths.Landing.path} className="">
              <span className='text-center align-items-center d-flex'>
                <IoIosArrowBack className='text-center align-items-center d-flex' /> 
                Вернуться на главную страницу
              </span>
            </Card.Link>
          </p>
          <Row className='justify-content-center shadow-soft border rounded border-light' style={{backgroundColor: '#F5F5F5'}}>
            <Col xs={12} className='d-flex align-items-center justify-content-center' style={{backgroundColor: '#F5F5F5'}}>
              <div className="mb-4 mb-lg-0 shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                <div className="text-center text-md-center mb-4 mt-md-0">
                  <h3 className="mb-0">Создать аккаунт</h3>
                </div>
                <Form noValidate validated={valid} onSubmit={handleSubmit} className="mt-4">
                  <Form.Group id="text" className="mb-4" >
                    <Form.Label>Имя</Form.Label>
                    <InputGroup>
                      <InputGroup.Text>
                        <MdDriveFileRenameOutline />
                      </InputGroup.Text>
                      <Form.Control autoFocus required type="text" placeholder="Ваше имя" onChange={handleChange('name')}/>
                      <Form.Control.Feedback type='invalid'>
                        Необходимо ввести имя
                      </Form.Control.Feedback>
                    </InputGroup>
                  </Form.Group>
                  <Form.Group id="email" className="mb-4">
                    <Form.Label>Email</Form.Label>
                    <InputGroup>
                      <InputGroup.Text>
                        <MdEmail/>
                      </InputGroup.Text>
                      <Form.Control autoFocus required type="email" placeholder="Ваш email" onChange={handleChange('email')}/>
                      <Form.Control.Feedback type='invalid'>
                        Необходимо ввести email
                      </Form.Control.Feedback>
                    </InputGroup>
                  </Form.Group>
                  <Form.Group id="password" className="mb-4">
                    <Form.Label>Пароль</Form.Label>
                    <InputGroup>
                      <InputGroup.Text>
                        <RiLockPasswordLine />
                      </InputGroup.Text>
                      <Form.Control required type="password" placeholder="Ваш пароль" onChange={handleChange('password')}/>
                      <Form.Control.Feedback type='invalid'>
                        Необходимо ввести пароль
                      </Form.Control.Feedback>
                    </InputGroup>
                  </Form.Group>
                  <FormCheck type="checkbox" className="d-flex mb-4">
                    <FormCheck.Input required id="terms" className="me-2" />
                    <FormCheck.Label htmlFor="terms">
                      Я соглашаюсь с <Card.Link>политикой конфиденциальности</Card.Link>
                    </FormCheck.Label>
                  </FormCheck>

                  <Button variant="primary" onClick={sendData} className="w-100">
                    Зарегистрироваться
                  </Button>
                </Form>

                <div className="d-flex justify-content-center align-items-center mt-4">
                  <span className="fw-normal">
                    Уже зарегистрированы? {' '}
                    <Card.Link as={Link} to={Paths.SignIn.path} className="fw-bold">
                      Войти
                    </Card.Link>
                  </span>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
        
        <Modal show={show} onHide={handleClose}
          size="sm"
          aria-labelledby="contained-modal-title-vcenter"
          centered
        >
        <Modal.Header closeButton>
            <Modal.Title id='contained-modal-title-vcenter'>Ошибка!</Modal.Title>
          </Modal.Header>
          <Modal.Body>{errorMsg}</Modal.Body>
          <Modal.Footer>
            <Button className='w-full' variant="primary" onClick={handleClose}>
              OK
            </Button>
          </Modal.Footer>
        </Modal>
      </section>
    </main>
    );
}

export default SignUp;