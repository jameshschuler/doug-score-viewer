<template>
  <table class="table mb-3">
    <thead>
      <tr>
        <th>Category</th>
        <th>Score</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <th>Styling</th>
        <td>{{ props.score?.styling }}</td>
      </tr>
      <tr>
        <th>Acceleration</th>
        <td>{{ props.score?.acceleration }}</td>
      </tr>
      <tr>
        <th>Handling</th>
        <td>{{ props.score?.handling }}</td>
      </tr>
      <tr>
        <th>Fun Factor</th>
        <td>{{ props.score?.funFactor }}</td>
      </tr>
      <tr>
        <th>Cool Factor</th>
        <td>{{ props.score?.coolFactor }}</td>
      </tr>
      <tr>
        <th>Total</th>
        <td>{{ props.score?.total }}</td>
      </tr>
    </tbody>
  </table>
</template>
<script setup lang="ts">
import { PropType } from "vue";
import { WeekendScore } from "../models/dougScore";

const props = defineProps({
  score: {
    type: Object as PropType<WeekendScore>,
    required: false,
  },
});
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";
.table {
  width: 75%;
  margin: 0 auto;

  @include mobile {
    width: 100%;
  }
}
</style>
