import React from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { Paths } from '../../routes';

const FirstScreen : React.FC = () => {
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

    return (
        <section id='main' className='flex flex-col items-center '>
            <div className='container mx-auto flex justify-between'>
                <div className='w-3/4 bg-lime-700'>
                  <div className='text-bold w-full text-white p-20 mt-10'>
                    <div className='text-8xl text'><p>GreenLife</p></div>
                    <div className='text-3xl text mt-20'><p>Приложение для формирования</p></div>
                    <div className='text-3xl text mt-1'><p>Ваших эко-привычек</p></div>
                    <div className='flex space-x-4 p-10'>
                        <button className=' w-40 h-16 bg-white hover:bg-slate-100 text-lime-700 border-2 rounded-lg' onClick={signIn}>
                            <p className='text-2lg font-medium'>Войти</p>
                        </button>
                        <button className='max-h-sm w-40 h-16 bg-white hover:bg-slate-100 text-lime-700 border-2 rounded-lg' onClick={signIn}>
                            <p className='text-2lg font-medium'>Зарегистрироваться</p>
                        </button>
                    </div>
                  </div>
                </div>
                <div className='flex justify-between'>
                  <img src='./img/planet.png' className=''></img>
                </div>
            </div>
        </section>
    )
}

export default FirstScreen;