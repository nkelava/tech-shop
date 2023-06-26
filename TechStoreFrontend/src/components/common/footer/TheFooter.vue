<script setup>
import { RouterLink } from "vue-router";
import ContactInfo from "@/components/common/ContactInfo.vue";
import ContactForm from "@/components/ContactForm.vue";

import EmailIcon from "@/assets/icons/contact/gmail16.png";
import PhoneIcon from "@/assets/icons/contact/phone16.png";
import FacebookIcon from "@/assets/icons/socials/facebook.png";
import InstagramIcon from "@/assets/icons/socials/instagram.png";
import YoutubeIcon from "@/assets/icons/socials/youtube.png";

const categories = ["laptops", "computers", "components", "peripherals", "software"];
</script>

<template>
  <footer class="pt-10 pb-2 px-16">
    <div class="footer-container">
      <div class="contact-container">
        <h3 class="footer__header">CONTACT US</h3>
        <contact-form class="contact__form" density="compact" isBtnAbsolute="true" />
      </div>
      <div class="categories-container">
        <h3 class="footer__header">CATEGORIES</h3>
        <router-link
          class="link"
          v-for="(category, index) in categories"
          :key="index"
          :to="category"
        >
          {{ category }}
        </router-link>
      </div>
      <div class="info-container">
        <h3 class="footer__header">CONTACT INFO</h3>
        <contact-info :imgUrl="EmailIcon" imgAlt="email icon">
          Email: tech-store@gmail.com
        </contact-info>
        <v-divider class="my-1" />
        <contact-info :imgUrl="PhoneIcon" imgAlt="phone icon"> Phone: 123-456-7890 </contact-info>
        <div class="socials">
          <a href="https://www.facebook.com/" title="Facebook">
            <img :src="FacebookIcon" class="socials__icon" title="Facebook" alt="facebook icon" />
          </a>
          <a href="https://www.instagram.com/" title="Instagram">
            <img
              :src="InstagramIcon"
              class="socials__icon"
              title="Instagram"
              alt="instagram icon"
            />
          </a>
          <a href="https://www.youtube.com/" title="Youtube">
            <img :src="YoutubeIcon" class="socials__icon" title="Youtube" alt="youtube icon" />
          </a>
        </div>
      </div>
    </div>
    <div class="footer-copyright">
      <h4>Copyright &copy; 2023 TechShop</h4>
    </div>
  </footer>
</template>

<style scoped>
.footer-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
  gap: 2rem;
  font-size: 1rem;
}

.footer__header {
  color: var(--ts-c-text-highlight);
  margin-bottom: 1rem;
}

.contact__form {
  max-width: 350px;
}

.categories-container {
  display: flex;
  flex-direction: column;
}

.categories-container a {
  color: var(--ts-c-text-light);
  text-transform: capitalize;
  margin-bottom: 5px;
}

.info-container {
  display: flex;
  flex-direction: column;
}

.socials {
  margin-top: 3rem;
}

.socials__icon {
  margin-right: 1.5rem;
}

.footer-copyright {
  border-top: 1px solid var(--ts-c-primary-dark);
  color: var(--ts-c-primary-mute);
  text-align: center;
  margin-top: 1rem;
  padding-top: 5px;
}

@media only screen and (max-width: 500px) {
  .contact-container,
  .categories-container,
  .info-container {
    text-align: center;
  }

  .contact-container *,
  .categories-container *,
  .info-container * {
    justify-content: center;
  }
}
</style>
