import React from 'react'
import { Link } from 'react-router-dom';
import { Paths } from '../../routes';

const LandHeader : React.FC = () => {
    return(
        <header className='flex mx-auto container px-8 justify-between border-2'>
          <nav className='flex mx-auto container ml-26 justify-between'>
          <a href='#main'><img src='./img/gl_logo.png' alt='Логотип' className='w-20'></img></a>
            <a href='#team' className='mt-2 text-lime-700 hover:text-lime-800 text-lg font-medium'>Команда</a>
            <a href='#targets' className='mt-2 text-lime-700 hover:text-lime-800 text-lg font-medium'>Цель проекта</a>
            <div className='flex box-content w-1/5'>
              <button className='border-2 box-border w-32 content-center bg-white hover:bg-slate-100 text-lime-700 rounded-lg text-lg font-medium' ><Link to={Paths.SignIn.path}>Вход</Link></button>
              <button className='border-2 content-center bg-lime-700 hover:bg-lime-900 text-white w-32 rounded-lg text-lg font-medium'><Link to={Paths.SignUp.path}>Регистрация</Link></button>
            </div>
          </nav>
        </header>
    )
}

export default LandHeader;