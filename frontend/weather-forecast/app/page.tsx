'use client'
import React from 'react';
import { Card, CardContent, Typography, TextField, Button, Grid, LinearProgress, Skeleton } from '@mui/material';
import Container from '@mui/material/Container';
import Box from '@mui/material/Box';
import { Alert } from '@mui/material';


// types.ts
export type Forecast = {
  number: number;
  name: string;
  startTime: string;
  endTime: string;
  isDaytime: boolean;
  temperature: number;
  temperatureUnit: string;
  temperatureTrend: null | string;
  detailedForecast: string;
  icon: string;
};

export type WeatherForecastData = {
  forecast: Forecast[];
};

const baseUrl =
  (process.env.NEXT_PUBLIC_BACKEND_API_URL ?? "https://localhost:7164");

export default function Home() {
  const [forecasts, setForecasts] = React.useState<Forecast[]>([]);
  const [street, setStreet] = React.useState('');
  const [city, setCity] = React.useState('');
  const [state, setState] = React.useState('');
  const [zip, setZip] = React.useState('');
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState('');

  console.log(process.env.NEXT_PUBLIC_BACKEND_API_URL)

  const handleSearch = async () => {
    try {
      setLoading(true);
      setError('');
      const response = await fetch(`${baseUrl}/WeatherForecast?Street=${encodeURIComponent(street)}&City=${encodeURIComponent(city)}&State=${encodeURIComponent(state)}&Zip=${encodeURIComponent(zip)}`);
      if (response.status === 404) {
        setError('No data found for the specified location.');
        setForecasts([]);
      } else if (response.status !== 200) {
        setError('An error occurred while fetching the data.');
        setForecasts([]);
      } else {
        const data: WeatherForecastData = await response.json();
        setForecasts(data.forecast);
      }
    } catch (error) {
      setError('An unexpected error occurred.');
      setForecasts([]);
    } finally {
      setLoading(false);
    }
  };
  return (
    <Container maxWidth="lg">
      {loading ? (
        <>
          <Grid item xs={12} md={12} lg={12}>
            <LinearProgress />
            <Skeleton />
            <Skeleton animation="wave" />
            <Skeleton animation={false} />
          </Grid>
        </>
      ) : (
        <>
          {error && <Alert severity="error">{error}</Alert>}
          <Box
            sx={{
              my: 4,
              display: 'flex',
              flexDirection: 'column',
              justifyContent: 'center',
              alignItems: 'center',
            }}
          >
            <Grid container spacing={2} alignItems="center" justifyContent="center">
              <Grid item><TextField label="Street" value={street} onChange={(e) => setStreet(e.target.value)} /></Grid>
              <Grid item><TextField label="City" value={city} onChange={(e) => setCity(e.target.value)} /></Grid>
              <Grid item><TextField label="State" value={state} onChange={(e) => setState(e.target.value)} /></Grid>
              <Grid item><TextField label="Zip" value={zip} onChange={(e) => setZip(e.target.value)} /></Grid>
              <Grid item><Button variant="contained" onClick={handleSearch}>Search</Button></Grid>
            </Grid>
            <Grid container spacing={2} style={{ marginTop: 20 }}>
              {forecasts.map((forecast) => (
                <Grid item xs={12} sm={6} md={4} key={forecast.number}>
                  <Card sx={{ display: 'flex', flexDirection: 'column' }}>
                    <CardContent sx={{ flexGrow: 1 }}>
                      <Typography variant="h5" component="div">
                        {forecast.name}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {forecast.startTime} - {forecast.endTime}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {forecast.temperature} {forecast.temperatureUnit}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {forecast.detailedForecast}
                      </Typography>
                    </CardContent>
                  </Card>
                </Grid>
              ))}
            </Grid>
          </Box>
        </>
      )}
    </Container>
  );
}