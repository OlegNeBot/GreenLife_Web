import React from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { Paths } from '../../routes';

const LandHeader : React.FC = () => {
  const navigate = useNavigate();

  const checkToken = (path: string) =>
  {
    let tokens = localStorage.getItem('token');
    if(tokens !== null) {
      navigate(Paths.HomePage.path);
      return true;
    } else {
      tokens = sessionStorage.getItem('token');
      if(tokens !== null) {
        navigate(Paths.HomePage.path);
      }
      else {
        navigate(path);
      }
    }
  }

  const signIn = () => {
    checkToken(Paths.SignIn.path);
  }

  const signUp = () => {
    checkToken(Paths.SignUp.path);
  }

    return(
        <header className='flex mx-auto container px-8 justify-between border-2'>
          <nav className='flex mx-auto container ml-26 justify-between'>
          <a href='#main'><img src='./img/gl_logo.png' alt='Логотип' className='w-20'></img></a>
            <a href='#team' className='mt-2 text-lime-700 hover:text-lime-800 text-lg font-medium'>Команда</a>
            <a href='#targets' className='mt-2 text-lime-700 hover:text-lime-800 text-lg font-medium'>Цель проекта</a>
            <div className='flex box-content w-1/5'>
              <button className='border-2 box-border w-32 content-center bg-white hover:bg-slate-100 text-lime-700 rounded-lg text-lg font-medium' onClick={signIn}>Вход</button>
              <button className='border-2 content-center bg-lime-700 hover:bg-lime-900 text-white w-32 rounded-lg text-lg font-medium' onClick={signUp}>Регистрация</button>
            </div>
          </nav>
        </header>
    )
}

export default LandHeader;