import axios from "axios";
import { autorun, makeAutoObservable } from "mobx";
import { CheckListModel } from "../models/CheckListModel";

class CheckListStore implements CheckListModel {
  Id: number = 0;
  CheckListName: {
    Name: string
  } = {
    Name: ''
  };
  ExecutionStatus: boolean = false;

  checklists: [] = [];

  constructor() {
    makeAutoObservable(this);
  }

  load = autorun(async () => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      return;
    } else {
    const url = 'http://localhost:8080/checklists';

    let token = localStorage.getItem('token');
    if(!token)
    {
      token = sessionStorage.getItem('token');
    }
    await axios.get(url, {
      headers: {
        'token': token!.toString(),
      }
    })
    .then((response) => {
      this.checklists = JSON.parse(response.headers['checklists']);

      if (localStorage.getItem('token')) {
          localStorage.setItem('token', response.headers['token']);
      } 
      else if (sessionStorage.getItem('token')) {
        sessionStorage.setItem('token', response.headers['token']);
      }
    })
    .catch((error) => {
      console.log(error);
    });
  }
  });
  
}

export default new CheckListStore();