import React from 'react'
import FirstScreen from '../components/landing/FirstScreen';
import LandHeader from '../components/landing/LandHeader';
import '../../build/dist/output.css';


const Landing : React.FC = () => {
    return(
        <>
        <link href="https://unpkg.com/tailwindcss@^2/dist/tailwind.min.css" rel="stylesheet"></link>
        <LandHeader/>
        <main className=''>
          <FirstScreen/>
        </main>
        </>
    )
}

export default Landing;