<script setup>
import { onMounted, ref } from "vue";
import { useRouter, RouterLink } from "vue-router";
import { axiosPrivate } from "@/api/axios";
import { useUserStore } from "@/store";
import UserIcon from "@/assets/icons/header/user.png";

const router = useRouter();
const userStore = useUserStore();
const userRole = ref(null);
const dropdownItems = [{ title: "My Account", to: "/user" }];

async function handleLogout() {
  try {
    await userStore.logoutUser();

    router.push("/");
  } catch (error) {
    if (error.response) {
      console.log(error.response);
    } else {
      console.log(`Error: ${error.message}`);
    }
  }
}

onMounted(async () => {
  const { token, refreshToken } = userStore.user;

  userRole.value = await axiosPrivate
    .post("/auth/role", { token, refreshToken })
    .then((response) => response.data)
    .catch((error) => console.log(error));
});
</script>

<template>
  <div class="text-center">
    <v-menu transition="scale-transition">
      <template v-slot:activator="{ props }">
        <v-btn v-bind="props" class="widget__btn" variant="text">
          <img :src="UserIcon" alt="favorites icon" class="dropdown__icon" />
        </v-btn>
      </template>

      <v-list class="dropdown-list">
        <v-list-item v-for="(item, i) in dropdownItems" :key="i">
          <router-link :to="item.to" class="link">{{ item.title }}</router-link>
        </v-list-item>
        <v-list-item v-if="userRole?.includes('Admin')">
          <router-link to="/admin" class="link"> Dashboard </router-link>
        </v-list-item>
        <v-list-item class="link" @click="handleLogout"> Logout </v-list-item>
      </v-list>
    </v-menu>
  </div>
</template>

<style scoped>
.dropdown-list {
  max-width: 300px;
}

.link {
  color: var(--ts-c-text-dark);
}

.link:hover {
  text-decoration: underline;
}
</style>
