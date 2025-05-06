const initApp = () => {};

const listAllProducts = async (e) => {
  e.preventDefault();

  const url = 'https://localhost:5010/api/products';

  try {
    // const user = JSON.parse(localStorage.getItem('user'));
    const user = JSON.parse(sessionStorage.getItem('user'));

    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'content-type': 'application/json',
        Authorization: 'bearer ' + user.token,
      },
    });

    if (response.ok) {
      const result = await response.json();
      console.log(result);
      if (result.success) {
        console.log(result.data);
      } else {
        console.error('Det gick inte att hämta produkter');
      }
    } else {
      console.error('Något hände', response.status, response.statusText);
    }
  } catch (error) {
    console.error(error);
  }
};

document.addEventListener('DOMContentLoaded', initApp);
document.querySelector('.btn').addEventListener('click', listAllProducts);
