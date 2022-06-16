import React from 'react'
import FirstScreen from '../components/landing/FirstScreen';
import LandHeader from '../components/landing/LandHeader';
import '../../build/dist/output.css';

const Landing : React.FC = () => {
    return(
        <>
        <LandHeader/>
        <main>
          <FirstScreen/>
        </main>
        </>
    )
}

export default Landing;