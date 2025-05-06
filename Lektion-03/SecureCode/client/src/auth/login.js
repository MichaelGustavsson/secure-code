// const form = document.querySelector('#login');
const initApp = () => {};

const login = async (e) => {
  e.preventDefault();
  const loginData = new FormData(e.target);
  const loginInfo = Object.fromEntries(loginData.entries());

  try {
    var url = 'https://localhost:5010/api/auth/login';
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
      },
      body: JSON.stringify(loginInfo),
    });

    if (response.ok) {
      const result = await response.json();
      console.log(result);
      const user = {
        email: result.email,
        token: result.token,
      };
      // localStorage.setItem('user', JSON.stringify(user));
      sessionStorage.setItem('user', JSON.stringify(user));

      // location.href = '../products/products.html';
      // document.cookie = 'token=' + result.token;
      // location.href = '../products/products.html?token=' + document.cookie;
    } else {
      console.log(response.status, response.statusText);
    }
  } catch (error) {
    console.error('Error', error);
  }
};

document.addEventListener('DOMContentLoaded', initApp);
document.querySelector('#login').addEventListener('submit', login);
