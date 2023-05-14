<script setup>
const categories = ["Laptops", "Computers", "Monitors", "Peripherals", "Memory"];
</script>

<template>
  <div class="footer">
    <div class="contact">
      <form class="contactform item">
        <div>
          <h3>CONTACT US</h3>
        </div>
        <div>
          <input type="text" placeholder="Enter email..." />
        </div>
        <div>
          <input type="text" placeholder="Enter subject..." />
        </div>
        <div>
          <textarea cols="50" rows="10" placeholder="Enter message here..." />
          <input type="submit" class="send" value="Send" />
        </div>
      </form>
    </div>
    <div class="categories item">
      <h3>CATEGORIES</h3>
      <a v-for="(category, index) in categories" :key="index" href="#">
        {{ category }}
      </a>
    </div>
    <div class="contact-info item">
      <h3>CONTACT INFO</h3>
      <p>Email:</p>
      <p>support@gmail.com</p>
      <p>Phone:</p>
      <p>123-456-7890</p>
    </div>
    <div class="copyright">
      <hr />
      Copyright © 2023 TechShop
    </div>
  </div>
</template>

<style scoped>
h3 {
  text-decoration: underline;
  color: var(--ts-c-text-highlight);
}
.footer {
  height: 100%;
  padding: 0.5rem 5rem;
  display: flex;
  justify-content: space-between;
}

.item {
  height: 100%;
}

.send {
  bottom: 10px;
  right: 20px;
}

.categories a {
  color: var(--ts-c-text-light);
  text-decoration: none;
  display: block;
}

.copyright {
  text-align: center;
  padding: 1rem;
}

.copyright hr {
  margin-top: 2rem;
}
</style>
