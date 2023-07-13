import axios from "axios";
import { autorun, makeAutoObservable, runInAction } from "mobx";
import jsPDF from 'jspdf';
import html2canvas from "html2canvas";

class AccountStore {
  Account: {
    Name: string,
    RegDate: string,
    Email: string,
    ScoreSum: number
  } = {
    Name: '',
    RegDate: '',
    Email: '',
    ScoreSum: 0
  };

  constructor() {
    makeAutoObservable(this, {}, {deep: true});
  }

  load = async() => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
    const url = 'https://localhost:7002/main';
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
        this.Account = response.data;
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

  notifyUser = (name: string) => {
    if (!("Notification" in window)) {
      alert("This browser does not support desktop notification");
    }

    else if (Notification.permission === "granted") {
      var notification = new Notification(name + ", ", {
        tag: "ache-mail",
        body: "Не забудьте отметить привычки, которые сегодня выполнили!",
        icon: "https://img.icons8.com/ios/50/000000/todo-list--v1.png"
      });
    }

    else if (Notification.permission !== "denied") {
      Notification.requestPermission().then((permission) => {
        if (permission === "granted") {
          var notification = new Notification(name + ", ", {
            tag: "ache-mail",
            body: "Не забудьте отметить привычки, которые сегодня выполнили!",
            icon: "https://img.icons8.com/ios/50/000000/todo-list--v1.png"
          });
        }
      });
    }
  }

  UserInfo : {
    Name: string,
    ScoreSum: number
  } = {
    Name: '',
    ScoreSum: 0
  };
  habits: number = 0;
  checklists: number = 0;

  loadReport = async () => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
      const url = 'https://localhost:7002/report';
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
        // TODO: Исправить получение из header'ов
        this.UserInfo = JSON.parse(response.headers['account']);
        this.habits = JSON.parse(response.headers['habits']);
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
  };

  giveReport = () => {
    let DATA: any = document.getElementById('report');
      html2canvas(DATA).then((canvas) => {
      let fileWidth = 210;
      let fileHeight = (canvas.height * fileWidth) / canvas.width;
      const FILEURI = canvas.toDataURL('image/png');
      let PDF = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      PDF.addImage(FILEURI, 'PNG', 0, position, fileWidth, fileHeight);
      PDF.save('Отчёт по текущему прогрессу.pdf');
});
  }

  accountLoad = async() => {
    if (!(localStorage.getItem('token') || sessionStorage.getItem('token'))) {
      console.log("Failed");
      return;
    } else {
      const url = 'https://localhost:7002/account';
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
          this.Account = response.data;
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

export default new AccountStore();