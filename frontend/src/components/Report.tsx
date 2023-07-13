import React, { useEffect } from 'react';
import { Button, Table } from 'react-bootstrap';

import account from '../store/AccountStore';

export interface ReportModel {
  Name?: string,
  ScoreSum?: number,
  Habits?: number,
  CheckLists?: number
}

const Report : React.FC<ReportModel> = () => {
  useEffect(() => {
    account.loadReport();
  }, [])
  
  return(
    <>
      <Table striped bordered id='report'>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Кол-во баллов</th>
            <th>Привычек сформировано</th>
            <th>Чек-листов выполнено</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{account.UserInfo.Name}</td>
            <td>{account.UserInfo.ScoreSum}</td>
            <td>{account.habits}</td>
            <td>{account.checklists}</td>
          </tr>
        </tbody>
      </Table>
      <Button variant='primary w-full' onClick={() => {account.giveReport()}}>Скачать отчет</Button>
    </>
  );
}

export default Report;