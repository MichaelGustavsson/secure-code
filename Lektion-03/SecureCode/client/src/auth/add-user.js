const initApp = () => {};

const createUser = async (e) => {
  e.preventDefault();
  const userData = new FormData(e.target);
  const userInfo = Object.fromEntries(userData.entries());

  try {
    var url = 'https://localhost:5010/api/auth/register';
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
      },
      body: JSON.stringify(userInfo),
    });

    if (response.ok) {
      location.href = './login.html';
    } else {
      console.log(response.status, response.statusText);
    }
  } catch (error) {
    console.error('Error', error);
  }
};

document.addEventListener('DOMContentLoaded', initApp);
document.querySelector('#add-user').addEventListener('submit', createUser);
