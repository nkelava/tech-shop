<script setup>
import { onMounted, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "../../../constants/messages/delete";

const users = ref([]);
const toast = useToast();

onMounted(() => reloadUsers());

async function reloadUsers() {
  const resp = await axiosPrivate.get("/users/all").catch((error) => console.log(error));

  if (resp.status !== 200) return;
  users.value = resp.data;
  console.log("users: ", users.value);
}

async function deleteUser(userEmail) {
  await axiosPrivate
    .delete(`/users/${userEmail}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  await reloadUsers();
}

// TODO: Add pagination (10 items per page)
</script>
<template>
  <v-container class="dashboard__container">
    <v-row>
      <h2>User List</h2>
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">First Name</th>
            <th class="text-left">Last Name</th>
            <th class="text-left">Email</th>
            <th class="text-left">Confirmed</th>
            <th class="text-left">Admin</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(user, id) in users" :key="id">
            <td>{{ user?.info?.firstName }}</td>
            <td>{{ user?.info?.lastName }}</td>
            <td :title="user?.info?.email">{{ user?.info?.email }}</td>
            <td>{{ user?.info?.emailConfirmed }}</td>
            <td>{{ user?.isAdmin }}</td>
            <td class="table__actions">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="30"
                title="Delete"
                alt="Delete"
                @click="deleteUser(user?.info?.email)"
              />
            </td>
          </tr>
          <tr v-if="users.length < 1">
            <td>There are no records.</td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.dashboard__table {
  width: 100%;
  margin-top: 1rem;
  color: var(--ts-c-text-light);
  background: transparent;
  border: 1px solid var(--ts-c-primary-dark);
  border-radius: 10px;
}

thead {
  background: var(--ts-c-primary-dark);
}

tr {
  vertical-align: center;
}

th {
  color: var(--ts-c-text-light) !important;
}

td {
  max-width: 500px !important;
  overflow: hidden;
  text-overflow: ellipsis;
}

.table__actions {
  display: flex;
  align-items: center;
  gap: 1rem;
}
</style>
