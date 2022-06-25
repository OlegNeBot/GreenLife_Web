import React, {useState} from 'react';
import { Button, Col, Container, Row } from 'react-bootstrap';

import { observer } from 'mobx-react-lite';
import account from '../store/AccountStore';
import { useNavigate } from 'react-router';
import { Paths } from '../routes';

const Account : React.FC = observer(() => {
  const navigate = useNavigate();
  return(
    <Container>
      <Row>
        <Col>
          <h4>Имя: {account.Account.Name}</h4>
          <h4>Email: {account.Account.Email}</h4>
          <h4>Дата регистрации: {account.Account.RegDate}</h4>

        </Col>
      </Row>
      <Row>
        <Col>
          <h4>Текущий прогресс</h4>
          <Button onClick={() => {
            navigate(Paths.Report.path);
          }}
          >
            Сформировать отчет</Button>
        </Col>
      </Row>
    </Container>
  );
});

export default Account;