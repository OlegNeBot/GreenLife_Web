import axios from "axios";
import { autorun, makeAutoObservable, runInAction } from "mobx";
import { MemoModel } from "../models/MemoModel";

class MemoStore implements MemoModel {
  Id: number = 0;
  MemoName: string = '';
  MemoRef: string = '';

  memos: [] = [];

  constructor() {
    makeAutoObservable(this);
  }

  load = async() => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
    const url = 'https://localhost:7002/memos';

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
        this.memos = response.data;
      });

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
  };

}

export default new MemoStore();