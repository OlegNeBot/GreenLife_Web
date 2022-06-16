import React, {useState} from 'react';
import { Alert } from 'react-bootstrap';

const Settings : React.FC = () => {
  return(
    <>
      <Alert variant='success'>
        Версия продукта: 0.1.0
      </Alert>
    </>
  );
}

export default Settings;