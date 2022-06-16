import React, {useState} from 'react';
import { Button, Col, Container, Row } from 'react-bootstrap';

const Account : React.FC = () => {
  return(
    <Container>
      <Row>
        <Col>
          <h4>Имя: Олег</h4>
          <h4>Email: myemail@yandex.ru</h4>
          <h4>Дата регистрации: 15.06.2022</h4>

        </Col>
      </Row>
      <Row>
        <Col>
          <h4>Текущий прогресс</h4>
          <Button>Сформировать отчет</Button>
        </Col>
      </Row>
    </Container>
  );
}

export default Account;