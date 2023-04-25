<template>
  <nav>
    <!-- CONTACT INFO AND LOGIN/REGISTER -->
    <div class="top">
      <div>
        <p>
          <a href="mailto:someone@example.com">📧 Email: support@gmail.com</a> | 📞 Phone:
          123-456-7890
        </p>
      </div>
      <div>
        <p><button>Login</button> | <button>Register</button></p>
      </div>
    </div>

    <!-- SHOP LOGO, SEARCH BAR AND USER GREETING/ICON -->
    <div class="mid">
      <div>
        <h2>TechShop</h2>
      </div>
      <div>
        <input type="text" placeholder="Search..." />
        <button class="search-btn">🔍</button>
      </div>
      <div>
        <p>Hello, test@gmail.com 👤</p>
      </div>
    </div>

    <hr />

    <!-- CATEGORIES, FAVORITES, CART -->
    <div class="bottom">
      <div>
        <button class="categories-btn">☰ All Categories</button>
      </div>
      <div class="categories">
        <a>Laptops</a>
        <a>Computers</a>
        <a>Monitors</a>
        <a>Peripherals</a>
        <a>Memory</a>
        <a>ContactUs</a>
      </div>
      <div>
        <button>❤️</button>
        <button>🛒</button>
      </div>
    </div>
  </nav>
</template>

<style scoped>
nav {
  width: 100%;
}

button {
  background: none;
  border: none;
  cursor: pointer;
  font-size: 14px;
}

a {
  cursor: pointer;
  text-decoration: none;
}

nav,
button,
a {
  color: var(--my-p);
}

.top {
  padding: 0.5rem 5rem;
  font-size: 14px;
  height: 100%;
  background-color: #f1e2b3;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.mid {
  padding: 0.5rem 5rem;
  color: var(--my-s);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

input {
  width: 500px;
}

.search-btn {
  background-color: var(--my-t);
}

.bottom {
  padding: 0.5rem 5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.bottom,
.bottom button,
.bottom a {
  color: var(--my-s);
}

.bottom .categories-btn {
  color: var(--my-p);
  background-color: var(--my-s);
}

.categories a {
  margin: 20px;
}
</style>
