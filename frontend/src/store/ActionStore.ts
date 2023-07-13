import axios from "axios";
import { autorun, makeAutoObservable, runInAction } from "mobx";
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

  load = async () => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
    const url = 'https://localhost:7002/achievements';

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
      runInAction(() => {
        this.actions = response.data;
      });

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
  };
}

export default new ActionStore();