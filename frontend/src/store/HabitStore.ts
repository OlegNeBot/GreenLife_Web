import axios from 'axios';
import { autorun, makeAutoObservable, runInAction } from 'mobx';

import { HabitModel } from '../models/HabitModel';

import account from './AccountStore';
import checklist from './CheckListStore';
import action from './ActionStore';

import jsPDF from 'jspdf';
import html2canvas from "html2canvas";

class HabitStore implements HabitModel {
  Id: number = 0;
  HabitName: string = '';
  Total: number = 0;
  HabitPerformance: [
    {
      NumOfExecs: number,
      Executed: boolean
    }
 ] = [ {
  NumOfExecs: 0,
  Executed: false
  }
 ];
  HabitPhrase: {
    PhraseText: string
  } = {
    PhraseText: ''
  };

  habits: HabitModel[] = [];

  sortedHabits: HabitModel[] = [];

  chLHabits: HabitModel[] = [];

  constructor() {
    makeAutoObservable(this, {}, {deep:true});
  }

  load = async () => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
    const url = 'https://localhost:7002/habits';
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
        this.habits = response.data;
        this.sortedHabits = this.habits;
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

  sorting = (mode: string) => {
    if (mode === '1') {
      
    }
    else if (mode === '2') {
      
    }
    else if (mode === '3') {
      
    }
    else if (mode === '4') {
      
    }
  }

  mark = async (id: number, habits: HabitModel[]) => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
      const url = `https://localhost:7002/perform/` + id;

      let token = localStorage.getItem('token');
      if(!token) {
        token = sessionStorage.getItem('token');
      }

      await axios.put(url, {}, {
        headers: {
          'token': token!.toString(),
        }
      })
      .then((response) => {
        runInAction(() => {
          this.HabitPhrase = response.data;
          this.HabitPerformance[0].NumOfExecs = habits[id-1].HabitPerformance[0].NumOfExecs++;
        });

        action.load();

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
  }

  checkExecuted = (id: number) => {
    if(this.habits[id-1].HabitPerformance[0].NumOfExecs === this.habits[id-1].Total) {
      this.habits[id-1].HabitPerformance[0].Executed = true;
      account.accountLoad();
      checklist.load();
    }
  }

  createReport = () => {
    let DATA: any = document.getElementById('certificate');
      html2canvas(DATA).then((canvas) => {
      let fileWidth = 210;
      let fileHeight = (canvas.height * fileWidth) / canvas.width;
      const FILEURI = canvas.toDataURL('image/png');
      let PDF = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      PDF.addImage(FILEURI, 'PNG', 0, position, fileWidth, fileHeight);
      PDF.save('Сертификат.pdf');
      }
      );}


  Name?: string = '';
  hName?: string = '';

  loadCertificate = async (id: number) => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
      const url = 'https://localhost:7002/certificate/' + id;
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
        // TODO: Разобраться с заголовками.
        this.Name = JSON.parse(response.headers['account']);
        this.hName = JSON.parse(response.headers['habit']);

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
  }

  loadByCheckList = async (id: number) => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
      const url = 'https://localhost:7002/checklist/' + id;
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
          this.chLHabits = response.data;
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
  }
}

export default new HabitStore();