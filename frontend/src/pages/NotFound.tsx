import React from 'react';
import { Container, Row, Col, Card, Button, Image } from 'react-bootstrap';
import { useNavigate } from 'react-router';
import { Paths } from '../routes';

const NotFound : React.FC = () => {
  const navigate = useNavigate();
  return(
    <main>
      <section className="vh-100 d-flex align-items-center justify-content-center">
        <Container>
          <Row>
            <Col xs={12} className="text-center d-flex align-items-center justify-content-center">
              <div>
                <h1 className="text-primary mt-5">
                  Страница <span className="fw-bolder">не найдена!</span>
                </h1>
                <p className="lead my-4">
                  Похоже, что у Вас проблемы. Сообщите нам, если считаете, что это мы виноваты.
            </p>
                <Button variant="primary" className="animate-hover" onClick={() => {navigate(Paths.Landing.path);}}>
                  Вернуться на главную
                </Button>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </main>
  );
}

export default NotFound;