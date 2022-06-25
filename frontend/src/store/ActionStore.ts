import axios from "axios";
import { autorun, makeAutoObservable } from "mobx";
import { ActionModel } from "../models/ActionModel";

class ActionStore implements ActionModel {
  Id: number = 0;
  ActionDate: string = '';
  Action: {
    ActionName: string,
    ActionType: {
      TypeName: string
    }
  } = {
    ActionName: '',
    ActionType: {
      TypeName: ''
    }
  };
  Account: {
    Name: string
  } = {
    Name: ''
  }

  actions: [] = [];

  constructor() {
    makeAutoObservable(this, {}, {deep: true});
  }

  load = autorun(async () => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      return;
    } else {
    const url = 'http://localhost:8080/achievements';

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
      this.actions = JSON.parse(response.headers['actions']);

      if (localStorage.getItem('token')) {
          localStorage.setItem('token', response.headers['token']);
      } 
      else if (sessionStorage.getItem('token')) {
        sessionStorage.setItem('token', response.headers['token']);
      }
    })
    .catch((error) => {
      alert(error);
    });
  }
  });
}

export default new ActionStore();