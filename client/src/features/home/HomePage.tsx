import { Box, Typography, Button } from '@mui/material';
import Slider from 'react-slick';
import { styled } from '@mui/system';

const Overlay = styled(Box)(({ theme }) => ({
    position: 'absolute',
    top: 0,
    left: 0,
    width: '100%',
    height: '100%',
    background: 'linear-gradient(to bottom, rgba(0,0,0,0.5), rgba(0,0,0,0.5))',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    color: theme.palette.common.white,
    textAlign: 'center',
    padding: theme.spacing(2),
}));

const HeroImage = styled('div')({
    position: 'relative',
    display: 'block',
    width: '100%',
    maxHeight: 500,
    overflow: 'hidden',
});

const CallToAction = styled(Button)(({ theme }) => ({
    marginTop: theme.spacing(3),
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.common.white,
    padding: theme.spacing(1.5, 4),
    fontSize: '1.2rem',
    transition: 'background-color 0.3s ease',
    '&:hover': {
        backgroundColor: theme.palette.primary.dark,
    },
}));

export default function HomePage() {
    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
    };

    return (
        <>
            <Slider {...settings}>
                <HeroImage>
                    <img src="/images/hero1.jpg" alt="hero" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
                    <Overlay>
                        <Typography variant='h3' sx={{ fontWeight: 'bold' }}>Discover Our Latest Collection</Typography>
                    </Overlay>
                </HeroImage>
                <HeroImage>
                    <img src="/images/hero2.jpg" alt="hero" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
                    <Overlay>
                        <Typography variant='h3' sx={{ fontWeight: 'bold' }}>Exclusive Deals Just for You</Typography>
                    </Overlay>
                </HeroImage>
                <HeroImage>
                    <img src="/images/hero3.jpg" alt="hero" style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
                    <Overlay>
                        <Typography variant='h3' sx={{ fontWeight: 'bold' }}>Shop the Trendiest Styles</Typography>
                    </Overlay>
                </HeroImage>
            </Slider>
            <Box display='flex' flexDirection='column' alignItems='center' sx={{ p: 4 }} >
                <Typography variant='h1' sx={{ fontWeight: 'bold', mb: 2 }}>Welcome to the Store</Typography>
                <CallToAction variant='contained' href="/catalog">Shop Now</CallToAction>
            </Box>
        </>
    );
}
